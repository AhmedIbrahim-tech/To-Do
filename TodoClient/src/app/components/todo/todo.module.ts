import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TodoRoutingModule } from './todo-routing.module';
import { CreateComponent } from './create/create.component';
import { UpdateComponent } from './update/update.component';
import { DeleteComponent } from './delete/delete.component';
import { ListComponent } from './list/list.component';
import { MaterialModule } from 'src/app/shared/material.module';
import { UpdateListComponent } from './update-list/update-list.component';


@NgModule({
  declarations: [
    CreateComponent,
    UpdateComponent,
    DeleteComponent,
    ListComponent,
    UpdateListComponent
  ],
  imports: [
    CommonModule,
    TodoRoutingModule,
    MaterialModule
  ]
})
export class TodoModule { }
