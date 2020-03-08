import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { UserService, User } from '../user.service';
import { StreamService } from '../stream.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-start-stream',
  templateUrl: './start-stream.component.html',
  styleUrls: ['./start-stream.component.css']
})
export class StartStreamComponent implements OnInit {
  private currentUser: User;
  startStreamForm;

  constructor(
    private streamService: StreamService,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { 
    this.startStreamForm = this.formBuilder.group({
      title: '',
      description: ''
    });
  }

  ngOnInit() {
    // this.userService.currentUser
    //   .subscribe(user => this.currentUser = user);
    this.currentUser = this.userService.currentUser.getValue();
    console.debug(`Streamer key: ${this.currentUser ? this.currentUser.streamerKey : null}`);
  }

  onSubmit(newStreamInfo) {
    newStreamInfo.broadcaster = this.currentUser.username;
    this.streamService.startStream(newStreamInfo)
      .then(() => this.router.navigate(['']));
  }

}
