//! Angular Modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

//! Material Modules
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatDividerModule } from '@angular/material/divider';

import { ToastrModule } from 'ngx-toastr';

//! Custom Components
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { LoginComponent } from './login/login.component';
import { SearchComponent } from './search/search.component';
import { FriendshipComponent } from './friendship/friendship.component';
import { FriendshipsComponent } from './friendships/friendships.component';
import { FollowersComponent } from './followers/followers.component';
import { FollowingsComponent } from './followings/followings.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    RegisterComponent,
    ProfileComponent,
    LoginComponent,
    SearchComponent,
    FriendshipComponent,
    FriendshipsComponent,
    FollowersComponent,
    FollowingsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatToolbarModule,
    MatButtonModule,
    MatTableModule,
    MatCardModule,
    MatInputModule,
    MatIconModule,
    MatDialogModule,
    MatSortModule, 
    MatPaginatorModule,
    MatSidenavModule,
    MatDividerModule,
    ToastrModule.forRoot({
      timeOut: 1500,
      easing: 'ease-in',
      progressBar: true,
      closeButton: true,
      positionClass:'toast-bottom-right',
      progressAnimation: 'decreasing'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
