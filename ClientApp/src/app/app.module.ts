import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { PlayerComponent } from './player/player.component';
import { LoginComponent } from './login/login.component';
import { CurrentUserComponent } from './current-user/current-user.component';
import { RegistrationComponent } from './registration/registration.component';
import { StartStreamComponent } from './start-stream/start-stream.component';
import { StreamListComponent } from './stream-list/stream-list.component';
import { StreamListElementComponent } from './stream-list-element/stream-list-element.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    PlayerComponent,
    LoginComponent,
    CurrentUserComponent,
    RegistrationComponent,
    StartStreamComponent,
    StreamListComponent,
    StreamListElementComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'player', component: PlayerComponent },
      { path: 'login', component: LoginComponent },
      { path: 'signup', component: RegistrationComponent },
      { path: 'start', component: StartStreamComponent },
      { path: 'stream/:streamerName', component: PlayerComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
