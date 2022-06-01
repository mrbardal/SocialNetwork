import { Component, OnInit } from '@angular/core';
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
  constructor(
    private friendService: FriendshipService,
    public userService: UserService) {

    this.friendships = [];
  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.friendService
      .getFriendships(this.userService.userName!)
      .subscribe((data) => {
        console.log(data);
        if (data != null) {
          this.friendships = data;
        }
      });
  }

  confirm(requester: string) {
    this.friendService.updateFriendship(
      new Friendship(requester, this.userService.userName!, 2)).subscribe(() => {
        this.load();
      });
  }

  reject(requester: string) {
    this.friendService.updateFriendship(
      new Friendship(requester, this.userService.userName!, 3)).subscribe(() => {
        this.load();
      });
  }
}
