import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SubjectToReturnDto, CreateSubjectDto, UpdateSubjectDto } from '../../../core/models/subject.models';
import { Result, PaginatedResult } from '../../../shared/shared-models';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  private readonly API_URL = 'http://localhost:5149/api/v1/subjects';
  
  constructor(private http: HttpClient) { }

  getAll(pageNumber: number = 0, pageSize: number = 6, search?: string): Observable<Result<PaginatedResult<SubjectToReturnDto>>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());
    
    if (search) {
      params = params.set('search', search);
    }

    return this.http.get<Result<PaginatedResult<SubjectToReturnDto>>>(this.API_URL, { params });
  }

  getAllNoPagination(search?: string): Observable<Result<SubjectToReturnDto[]>> {
    let params = new HttpParams();
    if (search) {
      params = params.set('search', search);
    }

    return this.http.get<Result<SubjectToReturnDto[]>>(`${this.API_URL}/all`, { params });
  }

  getById(id: number): Observable<Result<SubjectToReturnDto>> {
    return this.http.get<Result<SubjectToReturnDto>>(`${this.API_URL}/${id}`);
  }

  create(dto: CreateSubjectDto): Observable<Result<SubjectToReturnDto>> {
    return this.http.post<Result<SubjectToReturnDto>>(this.API_URL, dto);
  }

  update(dto: UpdateSubjectDto): Observable<Result<SubjectToReturnDto>> {
    return this.http.put<Result<SubjectToReturnDto>>(this.API_URL, dto);
  }

  delete(id: number): Observable<Result<SubjectToReturnDto>> {
    return this.http.delete<Result<SubjectToReturnDto>>(`${this.API_URL}/${id}`);
  }
}
