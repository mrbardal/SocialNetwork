import { Component, OnInit } from '@angular/core';
import { FriendshipService } from '../services/friendships.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.scss']
})
export class FollowersComponent implements OnInit {

  friendships: string[];
  constructor(
    private friendService: FriendshipService,
    private userService: UserService) {

    this.friendships = [];
  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.friendService
      .getFollowers(this.userService.userName!)
      .subscribe((data) => {
        console.log(data);
        if (data != null) {
          this.friendships = data;
        }
      });
  }

}
