import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { User } from 'src/app/_models/user';
import { AdminService } from 'src/app/_services/admin.service';
import { RolesModalComponent } from 'src/app/modals/roles-modal/roles-modal.component';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users: User[] = []
  bsModalRef: BsModalRef<RolesModalComponent> = new BsModalRef<RolesModalComponent>()
  availableRoles = ['Administrator', 'Moderator', 'Member']

  constructor(private bsModalService: BsModalService, private adminService: AdminService) { }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  openRolesModal(user: User) {
    const modalOptions: ModalOptions = {
      class: 'modal-dialog-centered',
      initialState: {
        user,
        availableRoles: this.availableRoles,
        selectedRoles: [...user.roles]
      },
    }
    this.bsModalRef.onHide?.subscribe({
      next: () => {
        const isConfirmUpdate = this.bsModalRef.content?.isConfirmUpdate
        const selectedRoles = this.bsModalRef.content?.selectedRoles.join(',')
        if (isConfirmUpdate && selectedRoles && selectedRoles !== "")
          this.adminService.updateUserRoles(user.username, selectedRoles).subscribe({
            next: response => user.roles = response
          })
      }
    })
  }
}