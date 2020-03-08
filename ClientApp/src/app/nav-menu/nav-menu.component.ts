import { Component } from '@angular/core';
import { UserService, User } from '../user.service';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  currentUser: User;

  constructor(private userService: UserService) {  
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  ngOnInit() {
    this.userService.currentUser
      .subscribe((user) => {
        this.currentUser = user;
        console.debug(`Current user: ${this.currentUser ? this.currentUser.username : null}`);
      });
  }
}
