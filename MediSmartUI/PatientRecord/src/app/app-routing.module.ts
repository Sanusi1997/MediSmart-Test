import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RecordFormComponent } from './record/record-form/record-form.component';
import { RecordComponent } from './record/record.component';

const routes: Routes = [
  {path:"records", component: RecordComponent},
  {path:"records/addRecord", component: RecordFormComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
