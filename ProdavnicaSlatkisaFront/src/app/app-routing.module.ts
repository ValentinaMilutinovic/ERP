import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { filter } from 'rxjs';
import { FilterssComponent } from './filterss/filterss.component';
import { TopNavComponent } from './layout/top-nav/top-nav.component';
import { ProductsComponent } from './products/products.component';
import { ViewProizvodComponent } from './products/view-proizvod/view-proizvod.component';
import { LoginComponent } from './login-reg/login/login.component';
import { CartComponent } from './cart/cart.component';
import { RegisterComponent } from './login-reg/register/register.component';
import { AddBrandComponent } from './admin-panel/add-brand/add-brand.component';
import { AddTipproizvodaComponent } from './admin-panel/add-tipproizvoda/add-tipproizvoda.component';
import { AddProizvodComponent } from './admin-panel/add-proizvod/add-proizvod.component';
import { DashboardComponent } from './admin-panel/dashboard/dashboard.component';
import { ProizvodViewComponent } from './admin-panel/view-list/proizvod-view/proizvod-view.component';
import { ProizvodjacViewComponent } from './admin-panel/view-list/proizvodjac-view/proizvodjac-view.component';
import { TipproizvodaViewComponent } from './admin-panel/view-list/tipproizvoda-view/tipproizvoda-view.component';
import { RacunViewComponent } from './admin-panel/view-list/racun-view/racun-view.component';
import { ProductUpdateComponent } from './admin-panel/update/product-update/product-update.component';
import { ProizvodjacUpdateComponent } from './admin-panel/update/proizvodjac-update/proizvodjac-update.component';
import { RacunUpdateComponent } from './admin-panel/update/racun-update/racun-update.component';
import { TipproizvodaUpdateComponent } from './admin-panel/update/tipproizvoda-update/tipproizvoda-update.component';
import { KupacViewComponent } from './admin-panel/view-list/kupac-view/kupac-view.component';
import { PaymentComponent } from './cart/payment/payment.component';
import { ShippingInfoComponent } from './cart/shipping-info/shipping-info.component';
import { AdminLoginComponent } from './login-reg/login/admin-login/admin-login.component';

const routes: Routes = [
  {
    path:'',
    component: ProductsComponent
  },
  {
    path:'api/Proizvod',
    component:ProductsComponent
  },
  {
    path:'filterss',
    component:FilterssComponent
  },
  {
    path:'api/Proizvod/:id',
    component:ViewProizvodComponent
  },
  {
    path:'api/Kupac/login',
    component:LoginComponent
  },
  {
    path:'api/Korpa/1',
    component:CartComponent
  },
  {
    path:'api/Kupac/register',
    component:RegisterComponent
  },
  {
    path:'api/proizvodjacAdd',
    component:AddBrandComponent
  },
  {
    path:'api/tipProizvodaAdd',
    component:AddTipproizvodaComponent
  },
  {
    path:'api/ProizvodAdd',
    component:AddProizvodComponent
  },
  {
    path:'api/dashboard',
    component:DashboardComponent
  },
  {
    path:'api/ProizvodView',
    component:ProizvodViewComponent
  },
  {
    path:'api/ProizvodjacView',
    component:ProizvodjacViewComponent
  },

  {
    path:'api/TipProizvodaView',
    component:TipproizvodaViewComponent
  },
  {
    path:'api/RacunView',
    component:RacunViewComponent
  },
  {
    path:'api/KupcaView',
    component:KupacViewComponent
  },
  {
    path:'api/Proizvod/update/:id',
    component:ProductUpdateComponent
  },
  {
    path:'api/TipProizvodum/:id',
    component:TipproizvodaUpdateComponent
  },
  {
    path:'api/Proizvodjac/:id',
    component:ProizvodjacUpdateComponent
  },
  {
    path:'api/Racun/RacunId/:id',
    component:RacunUpdateComponent
  },
  {
    path:'Placanje',
    component:PaymentComponent
  },
  {
    path:'Nastavi',
    component:ShippingInfoComponent
  },
  {
    path:'api/Administrator/login',
    component:AdminLoginComponent
  }





];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
