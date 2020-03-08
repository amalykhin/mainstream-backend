import { Component, OnInit } from '@angular/core';
import { StreamService } from '../stream.service';

@Component({
  selector: 'app-stream-list',
  templateUrl: './stream-list.component.html',
  styleUrls: ['./stream-list.component.css']
})
export class StreamListComponent implements OnInit {
  streams;


  constructor(private streamService: StreamService) { }

  ngOnInit() {
    this.streamService.getStreams()
      .subscribe(streams => this.streams = streams);
  }

}
