import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result, PaginatedResult } from '../../../shared/shared-models';
import { StudentToReturnDto, CreateStudentDto, UpdateStudentDto } from '../../../core/models/student.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private readonly API_URL = 'http://localhost:5149/api/v1/students';

  constructor(private http: HttpClient) { }

  getAll(pageNumber: number = 0, pageSize: number = 6, search?: string): Observable<Result<PaginatedResult<StudentToReturnDto>>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());
    
    if (search) {
      params = params.set('search', search);
    }

    return this.http.get<Result<PaginatedResult<StudentToReturnDto>>>(this.API_URL, { params });
  }

  getAllNoPagination(search?: string): Observable<Result<StudentToReturnDto[]>> {
    let params = new HttpParams();
    if (search) {
      params = params.set('search', search);
    }

    return this.http.get<Result<StudentToReturnDto[]>>(`${this.API_URL}/all`, { params });
  }

  getById(id: number): Observable<Result<StudentToReturnDto>> {
    return this.http.get<Result<StudentToReturnDto>>(`${this.API_URL}/${id}`);
  }

  create(dto: CreateStudentDto): Observable<Result<StudentToReturnDto>> {
    return this.http.post<Result<StudentToReturnDto>>(this.API_URL, dto);
  }

  update(dto: UpdateStudentDto): Observable<Result<StudentToReturnDto>> {
    return this.http.put<Result<StudentToReturnDto>>(`${this.API_URL}/${dto.id}`, dto);
  }

  delete(id: number): Observable<Result<StudentToReturnDto>> {
    return this.http.delete<Result<StudentToReturnDto>>(`${this.API_URL}/${id}`);
  }
}
