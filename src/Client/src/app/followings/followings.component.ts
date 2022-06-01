import { Component, OnInit } from '@angular/core';
import { FriendshipService } from '../services/friendships.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-followings',
  templateUrl: './followings.component.html',
  styleUrls: ['./followings.component.scss']
})
export class FollowingsComponent implements OnInit {

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
      .getFollowings(this.userService.userName!)
      .subscribe((data) => {
        console.log(data);
        if (data != null) {
          this.friendships = data;
        }
      });
  }


}
