export interface CreateStudentDto {
    firstName: string;
    secondName: string;
    lastName?: string;
    dateOfBirth: string;
    isMale: boolean;
    email: string;
    phoneNumber: string;
    subjectIds: number[];
}

export interface UpdateStudentDto {
    id: number;
    firstName: string;
    secondName: string;
    lastName?: string;
    dateOfBirth: string;
    isMale: boolean;
    email: string;
    phoneNumber: string;
    subjectIds: number[];
}

export interface StudentToReturnDto {
    id: number;
    firstName: string;
    secondName: string;
    lastName?: string;
    dateOfBirth: string;
    isMale: boolean;
    email: string;
    phoneNumber: string;
    subjects: StudentSubjectDto[];
}

export interface StudentSubjectDto {
    id: number;
    code: string;
}