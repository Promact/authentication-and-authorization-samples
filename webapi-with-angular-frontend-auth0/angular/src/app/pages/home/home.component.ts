import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  constructor(public auth: AuthService) {
    auth.user$.subscribe((user) => console.log(user));
    auth.idTokenClaims$.subscribe((claims) => console.log(claims));
    auth.appState$.subscribe((state) => console.log(state));
    auth.getAccessTokenSilently().subscribe((token) => console.log(token));
    auth.isAuthenticated$.subscribe((isAuthenticated) => console.log(isAuthenticated));

  }
}
