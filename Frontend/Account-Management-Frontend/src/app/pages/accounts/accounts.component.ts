import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html'
})
export class AccountsComponent implements OnInit {

  accounts: any[] = [];

  newAccount = {
    accountName: '',
    balance: 0
  };

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.loadAccounts();
  }

  // 📥 Get all accounts
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

  // ➕ Create account
  createAccount() {
    this.accountService.createAccount(this.newAccount).subscribe({
      next: () => {
        this.loadAccounts();
        this.newAccount = { accountName: '', balance: 0 };
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  // ❌ Delete account
  deleteAccount(id: number) {
    this.accountService.deleteAccount(id).subscribe({
      next: () => {
        this.loadAccounts();
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
}