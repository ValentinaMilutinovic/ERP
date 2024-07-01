import { JwtHelperService, JwtInterceptor } from '@auth0/angular-jwt';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FilterssComponent } from './filterss/filterss.component';

// Add below imports to the Imports Section on the top of the page

import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSliderModule } from '@angular/material/slider';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
// Material Navigation
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
// Material Layout
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatListModule } from '@angular/material/list';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTreeModule } from '@angular/material/tree';
import { ToastrModule } from 'ngx-toastr';
// Material Buttons & Indicators
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatBadgeModule } from '@angular/material/badge';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatRippleModule } from '@angular/material/core';
// Material Popups & Modals
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
// Material Data tables
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { TopNavComponent } from './layout/top-nav/top-nav.component';

import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ProductsComponent } from './products/products.component';
import { ViewProizvodComponent } from './products/view-proizvod/view-proizvod.component';
import { SearchesComponent } from './searches/searches.component';
import { ClickOutsideDirective } from './clickOutside.directive';
import { BottomBarComponent } from './bottom-bar/bottom-bar.component';
import { LoginComponent } from './login-reg/login/login.component';
import { RegisterComponent } from './login-reg/register/register.component';
import { ProfileComponent } from './login-reg/profile/profile.component';
import { CartComponent } from './cart/cart.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AddBrandComponent } from './admin-panel/add-brand/add-brand.component';
import { AddTipproizvodaComponent } from './admin-panel/add-tipproizvoda/add-tipproizvoda.component';
import { AddProizvodComponent } from './admin-panel/add-proizvod/add-proizvod.component';

import { JWT_OPTIONS} from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';
import { LoginServiceService } from './services/login-service.service';
import { DashboardComponent } from './admin-panel/dashboard/dashboard.component';
import { ProizvodViewComponent } from './admin-panel/view-list/proizvod-view/proizvod-view.component';
import { TipproizvodaViewComponent } from './admin-panel/view-list/tipproizvoda-view/tipproizvoda-view.component';
import { ProizvodjacViewComponent } from './admin-panel/view-list/proizvodjac-view/proizvodjac-view.component';
import { RacunViewComponent } from './admin-panel/view-list/racun-view/racun-view.component';
import { ProductUpdateComponent } from './admin-panel/update/product-update/product-update.component';
import { ProizvodjacUpdateComponent } from './admin-panel/update/proizvodjac-update/proizvodjac-update.component';
import { TipproizvodaUpdateComponent } from './admin-panel/update/tipproizvoda-update/tipproizvoda-update.component';
import { RacunUpdateComponent } from './admin-panel/update/racun-update/racun-update.component';
import { KupacViewComponent } from './admin-panel/view-list/kupac-view/kupac-view.component';
import { PaymentComponent } from './cart/payment/payment.component';
import { ShippingInfoComponent } from './cart/shipping-info/shipping-info.component';
import { DialogComponent } from './dialog/dialog.component';
import { AdminRegComponent } from './admin-panel/admin-reg/admin-reg.component';
import { AdminLoginComponent } from './login-reg/login/admin-login/admin-login.component';

@NgModule({
  declarations: [
    AppComponent,
    TopNavComponent,
    FilterssComponent,
    ProductsComponent,
    ViewProizvodComponent,
    SearchesComponent,
    ClickOutsideDirective,
    BottomBarComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    CartComponent,
    AddBrandComponent,
    AddTipproizvodaComponent,
    AddProizvodComponent,
    DashboardComponent,
    ProizvodViewComponent,
    TipproizvodaViewComponent,
    ProizvodjacViewComponent,
    RacunViewComponent,
    ProductUpdateComponent,
    ProizvodjacUpdateComponent,
    TipproizvodaUpdateComponent,
    RacunUpdateComponent,
    KupacViewComponent,
    PaymentComponent,
    ShippingInfoComponent,
    DialogComponent,
    AdminRegComponent,
    AdminLoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    // Add below modules to the imports array

MatAutocompleteModule,
MatCheckboxModule,
MatDatepickerModule,
MatNativeDateModule,
MatFormFieldModule,
MatInputModule,
MatRadioModule,
MatSelectModule,
MatSliderModule,
MatSlideToggleModule,
MatMenuModule,
MatSidenavModule,
MatToolbarModule,
MatCardModule,
MatDividerModule,
MatExpansionModule,
MatGridListModule,
MatListModule,
MatStepperModule,
MatTabsModule,
MatTreeModule,
MatButtonModule,
MatButtonToggleModule,
MatBadgeModule,
MatChipsModule,
MatIconModule,
MatProgressSpinnerModule,
MatProgressBarModule,
MatRippleModule,
MatBottomSheetModule,
MatDialogModule,
MatSnackBarModule,
MatTooltipModule,
MatPaginatorModule,
MatSortModule,
MatTableModule,
HttpClientModule,
ReactiveFormsModule,
ToastrModule.forRoot()
  ],
  schemas:[
    CUSTOM_ELEMENTS_SCHEMA
  ],
  providers: [
    LoginServiceService,
    JwtInterceptor,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,
    CookieService
  ],

  bootstrap: [AppComponent],

})
export class AppModule { }
