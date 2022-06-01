import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Friendship } from '../models/friendship.model';
import { FriendshipService } from '../services/friendships.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-friendships',
  templateUrl: './friendships.component.html',
  styleUrls: ['./friendships.component.scss']
})
export class FriendshipsComponent implements OnInit {

  friendships: string[];
  isFirstLoad: boolean;

  constructor(
    private toastr: ToastrService,
    private friendService: FriendshipService,
    public userService: UserService) {

    this.friendships = [];

    this.isFirstLoad = true;
  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.friendService
      .getFriendships(this.userService.userName!)
      .subscribe((data) => {
        if (data.length == 0 && this.isFirstLoad) {
          this.toastr.info('No request awaiting to confirm!', 'Confirm');
          this.isFirstLoad = false;
        }

        this.friendships = data;
      });
  }

  confirm(requester: string) {
    this.friendService.updateFriendship(
      new Friendship(requester, this.userService.userName!, 2)).subscribe(() => {
        this.toastr.info('Request confirmed', 'Confirm');

        this.load();
      });
  }

  reject(requester: string) {
    this.friendService.updateFriendship(
      new Friendship(requester, this.userService.userName!, 3)).subscribe(() => {
        this.toastr.error('Request rejected', 'Reject');

        this.load();
      });
  }
}
