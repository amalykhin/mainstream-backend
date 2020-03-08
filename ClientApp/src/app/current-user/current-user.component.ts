import { Component, OnInit, Input } from '@angular/core';
import { UserService, User } from '../user.service';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-current-user',
  templateUrl: './current-user.component.html',
  styleUrls: ['./current-user.component.css']
})
export class CurrentUserComponent implements OnInit {
  @Input() user: BehaviorSubject<User>;
  
  constructor() { 
  }

  ngOnInit() {
  }

}
