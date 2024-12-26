export interface CreateSubjectDto {
    code: string;
    name: string;
    description?: string;
}

export interface UpdateSubjectDto {
    id: number;
    code: string;
    name: string;
    description?: string;
}
  
export interface SubjectToReturnDto {
    id: number;
    code: string;
    name: string;
    description?: string;
    studentsCount: number;
}