import { Component, OnInit, OnDestroy } from '@angular/core';
import { Location } from '@angular/common';
import { SidePanelService } from 'src/app/lecacy-app/core/services/side-panel.service';
import { Subscription } from 'rxjs';
import { LoaderService } from 'src/app/lecacy-app/core/services/loader.service';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { AppActionsService } from 'src/app/lecacy-app/core/services/app-actions.service';
declare var $: any;

@Component({
  selector: 'app-home',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css'],
  providers: [SidePanelService]
})
export class LayoutComponent implements OnInit, OnDestroy {
  sidePanelTitle: string;
  private subs: Subscription[] = [];
  private loaders: string[] = [];
  userRoles: string = "";

  constructor(public sidePanelService: SidePanelService, private router: Router,
    private loaderService: LoaderService, private authorizeService: AuthorizeService,
    private location: Location, private actionsService: AppActionsService) {
  }

  ngOnInit(): void {
    this.subs.push(this.sidePanelService.panelOpened.subscribe((e: any) => {
      if (e) {
        this.sidePanelTitle = e.title;
        this.showSidePanel();
      }
    }));
    this.subs.push(this.sidePanelService.panelClosed.subscribe(x => {
      this.hideSidePanel();
    }));

    this.subs.push(this.loaderService.statusChanged.subscribe(x => {
      if (this.loaders.includes(x)) {
        this.loaders = this.loaders.filter(l => l != x);
      } else {
        this.loaders.push(x);
      }

      if (this.loaders.length > 0) {
        document.getElementById("overlay").style.display = "block";
        //   $('#loadingModal').modal({
        //     show: true,
        //     backdrop: 'static',
        //     keyboard: false
        //   });
      } else {
        //$('#loadingModal').modal('hide');
        document.getElementById("overlay").style.display = "none";

      }

    }));

    this.subs.push(this.authorizeService.getUser().subscribe((user: any) => {
      this.userRoles = user.roles;
      if (this.userRoles?.includes('Technician'))
        this.router.navigate(['unauthorized']);
      this.actionsService.updateActionsPerUser(user);
    }));

    $(document).ready(() => {
      (document as any).runMasterScript();
    });
  }

  ngOnDestroy(): void {
    this.subs.forEach(sub => {
      sub.unsubscribe();
    });
  }

  showSidePanel() {
    $('#myModal2').modal({
      show: true,
      backdrop: 'static',
      keyboard: false
    });
  }

  hideSidePanel() {
    $('#myModal2').modal('hide');
  }
}