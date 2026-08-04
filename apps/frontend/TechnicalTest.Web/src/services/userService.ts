import type {ApiUser, UserUI} from '../types/user';

const API_URL = 'http://localhost:5000/api';

const mapUserToUI = (user: ApiUser): UserUI => {
    return {
        ...user,
        avatar: `https://api.dicebear.com/7.x/avataaars/svg?seed=${user.id}`,
        country: 'España'
    };
};

export const getUsers = async (): Promise<UserUI[]> => {
    const response = await fetch(`${API_URL}/users`);
    
    if(!response.ok) {
        throw new Error('Error al conectar con la API');
    }
    
    const data: ApiUser[] = await response.json();
    
    return data.map(mapUserToUI);
}