export interface LoginRequest {
    email: string;
    password: string;
}

export interface UserDto {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
}

export interface AuthResponseDto {
    token: string;
    user: UserDto;
}