import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ViewService } from '../../services/view.service';

@Component({
  selector: 'app-racun-update',
  templateUrl: './racun-update.component.html',
  styleUrls: ['./racun-update.component.css']
})
export class RacunUpdateComponent implements OnInit {
  racunId: number | undefined;

  constructor(private readonly viewService: ViewService, private readonly route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      this.racunId = idParam ? Number(idParam) : undefined;

      if (this.racunId) {
        this.viewService.getRacun(this.racunId)
        .subscribe(
          (successResponse)=>{
            console.log(successResponse);
          }
        );
      }
    });
  }
}
