import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { RecordServiceService } from 'src/app/shared/record-service.service';
import { Record } from 'src/app/models/record.model';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styles: [
  ]
})
export class RecordComponent implements AfterViewInit {
  Records: Record [];

  constructor(public service: RecordServiceService,
    private toastr: ToastrService) { }

  displayedColumns: string[] = ['no', 'name', 'occupation', 'provider'];
  dataSource =  new  MatTableDataSource<Record>(this.getRecordsList());

  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  } 
  getRecordsList() : Record[]{
    this.service.getRecord().subscribe(Record =>{
      this.Records = Record;
    });  
    return this.Records;
  }
  populateForm(selectedData: Record){
    this.service.formData = Object.assign({}, selectedData);
  }
  deleteRecord(patientId: number) {
    this.service.deleteRecord(patientId).subscribe(
      res => {
        this.toastr.info('Successfully deleted patient record', 'Patient Record',
          {
            timeOut: 6000,
            positionClass: 'toast-top-center'
          });
   
      },
      err => {
        this.toastr.error('Error deleting details', 'Error', {
          timeOut: 4000,
        });
      }
    );
  }

}
