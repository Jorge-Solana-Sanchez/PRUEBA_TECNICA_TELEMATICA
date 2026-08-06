export interface ApiUser{
    id: string;
    name: string;
    email: string;
    createdAt: string;
}

export interface UserUI extends ApiUser {
    avatar: string;
    country: string;
}

export type ModalMode = 'add' | 'edit' | null;

