import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(public service: UserService) { }

  ngOnInit(): void {
  }

  @Output() sidenavToggle = new EventEmitter();

  toggle() {
    this.sidenavToggle.emit();
  }
}
