import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-transfer',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './transfer.html',
  styleUrl: './transfer.css',
})
export class Transfer {

  accounts: any[] = [];

  transferData = {
    fromAccountId: 0,
    toAccountId: 0,
    amount: 0
  };

  constructor(private accountService: AccountService) {
    this.loadAccounts();
  }

  loadAccounts() {
    this.accountService.getAccounts().subscribe({
      next: (res: any) => {
        this.accounts = res;
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  transferMoney() {
    if (!this.transferData.fromAccountId || !this.transferData.toAccountId || this.transferData.amount <= 0) {
      return;
    }

    this.accountService.transfer(this.transferData).subscribe({
      next: () => {
        this.transferData.amount = 0;
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

}
