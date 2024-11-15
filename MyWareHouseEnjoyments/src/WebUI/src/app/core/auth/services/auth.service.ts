import {Injectable, OnDestroy} from '@angular/core';
import {HttpClient, HttpResponse} from '@angular/common/http';
import {environment} from '../../../../environments/environment';
import { shareReplay } from 'rxjs/operators';
import { tap } from 'rxjs/operators';
import {AuthenticationSuccessData} from '../models/login-data';
import {BehaviorSubject, Observable, Subscription} from 'rxjs';
import { SocialAuthService } from 'angularx-social-login';

@Injectable({
  providedIn: 'root'
})

export class AuthService implements OnDestroy {

  public signInState: Observable<AuthenticationSuccessData>;
  private _signInState = new BehaviorSubject<AuthenticationSuccessData>(null);
  private socialAuthSub: Subscription;

  constructor(private _http: HttpClient, private _socialAuth: SocialAuthService) {

    this.signInState = this._signInState.asObservable();

    const userData = this.getStoredUserData();
    if (userData != null) {
      this._signInState.next(userData);
    }
  }

  ngOnDestroy(): void {
    this.socialAuthSub.unsubscribe();
  }

  public authenticate(username: string, password: string): Observable<HttpResponse<AuthenticationSuccessData>> {
    const loginData = {
      username: username,
      password: password
    };

    return this._http.post<AuthenticationSuccessData>(`${environment.baseHost}/account/login`, loginData, { observe: 'response' })
      .pipe(
        tap(res => this.signIn(res.body)),
        shareReplay()
      );
  }

  private signIn(data: AuthenticationSuccessData) {
    const expiresAt = new Date();
    expiresAt.setTime(Date.now() + (data.expiresIn * 1000));

    localStorage.setItem('auth_userData', JSON.stringify(data));
    localStorage.setItem('auth_tokenString', `${data.tokenType} ${data.accessToken}`);
    localStorage.setItem('auth_tokenExpiresAt', expiresAt.getTime().toString());
    this._signInState.next(data);
  }

  public signOut() {
    localStorage.removeItem('auth_userData');
    localStorage.removeItem('auth_tokenString');
    localStorage.removeItem('auth_tokenExpiresAt');
    this._socialAuth.signOut().catch(_ => {});
    this._signInState.next(null);
  }

  public isSignedIn() {
    return this._signInState.value != null;
  }

  public getUserToken() {
    return localStorage.getItem('auth_tokenString');
  }

  public getValidityDays() {
    return (+localStorage.getItem('auth_tokenExpiresAt') - Date.now()) / 1000 / (3600 * 24);
  }

  private getStoredUserData(): AuthenticationSuccessData {
    return JSON.parse(localStorage.getItem('auth_userData')) as AuthenticationSuccessData;
  }
}
