import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Friendship } from '../models/friendship.model';
import { FriendshipService } from '../services/friendships.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-friendship',
  templateUrl: './friendship.component.html',
  styleUrls: ['./friendship.component.scss']
})
export class FriendshipComponent implements OnInit {

  addressee?: string;
  friendship: Friendship;

  constructor(
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private friendService: FriendshipService,
    public userService: UserService) {

    this.friendship = new Friendship('', '', 0);
  }

  ngOnInit(): void {
    this.addressee = this.route.snapshot.paramMap.get('addressee')!;

    this.load();
  }

  load() {
    this.friendService
      .getFriendship(this.userService.userName!, this.addressee!)
      .subscribe((data) => {
        if (data != null) {
          this.friendship = data;
        }
      });
  }

  request() {
    this.friendService.addFriendship(
      new Friendship(this.userService.userName!, this.addressee!, 1)).subscribe(() => {
        this.toastr.success("Add request successfully!", "Request");

        this.load();
      });
  }

  get status(): string {
    switch (this.friendship.statusId) {
      case 1:
        return ' Pending for ' + this.addressee;
      case 2:
        return this.addressee + ' is your Friend';
      case 3:
        return this.addressee + ' Reject you request!';
      default:
        return ' Connect to ' + this.addressee;
    }
  }

}
