import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Record } from 'src/app/models/record.model';
import { RecordServiceService } from 'src/app/shared/record-service.service';

@Component({
  selector: 'app-record-form',
  templateUrl: './record-form.component.html',
  styles: [
  ]
})
export class RecordFormComponent implements OnInit {
  Records: Record [];
  constructor( public service: RecordServiceService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
  }
  submit(form: NgForm) {
    if(this.service.formData.patientId == 0){
      this.insert(form);
    }else{
      this.update(form);
    }
  }
  insert(form: NgForm) {
    this.service.postRecord().subscribe(

      res => {
        this.toastr.success('Department successfully added', 'Department',
          {
            timeOut: 4000,
            positionClass: 'toast-top-center'
          });
          this.resetForm(form);
           location.reload();
      },
      err => {
        this.toastr.error('Error submiting details', 'Error', {
          timeOut: 4000,
        });
      }
    );
  }
  update(form: NgForm) {
    this.service.updateRecord().subscribe(
      res => {
        this.toastr.info('Successfully updated patient record', 'Patient Record',
          {
            timeOut: 6000,
            positionClass: 'toast-top-center'
          });

          this.resetForm(form);
          location.reload();
   
      },
      err => {
        this.toastr.error('Error updating details', 'Error', {
          timeOut: 4000,
        });
      }
    );
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new Record();

  }
  refreshList (){
    this.service.getRecord().subscribe(Record =>{
      this.Records = Record;
    });  

  }
}
