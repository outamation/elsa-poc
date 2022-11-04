import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StepsModule } from 'primeng/steps';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { SidebarModule } from 'primeng/sidebar';
import { MenuModule } from 'primeng/menu';
import { RippleModule } from 'primeng/ripple';
import { HttpClientModule } from '@angular/common/http';
import { AccordionModule } from 'primeng/accordion';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { FormsModule } from '@angular/forms';
import { CalendarModule } from 'primeng/calendar';
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [CommonModule, BrowserModule, AppRoutingModule, BrowserAnimationsModule, ProgressSpinnerModule, FormsModule, CalendarModule,
    StepsModule, ToastModule, ButtonModule, CardModule, SidebarModule, MenuModule, RippleModule, AccordionModule,
    HttpClientModule
  ],
  providers: [MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
