import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Url } from 'url';

import videojs from 'video.js';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {
  private readonly streamServerUrl = 'http://167.172.107.135:8080/livestreams';
  @ViewChild('video', {static: false}) videoElement: ElementRef;
  player;

  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
  }

  ngAfterViewInit() {
    const stateData = history.state.data;
    const streamKey = stateData.broadcaster.streamerKey;
    const streamUrl = `${this.streamServerUrl}/${streamKey}.m3u8`;
    this.player = videojs(this.videoElement.nativeElement);
    this.player.src({type: "application/vnd.apple.mpegurl", src: streamUrl}) 
    this.player.ready(() => this.player.play());
    // console.log(this.player.tech().hls);
  }

  ngOnDestroy() {
    this.player.dispose();
  }
}
