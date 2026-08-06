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
};

export const createUser = async (userData: {name: string; email: string}): Promise<UserUI> => {
    const response = await fetch(`${API_URL}/users`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData),
    });
    if(!response.ok) {
        throw new Error('Error al crear el usuario en la API');
    }
    
    const createdUser: ApiUser = await response.json();
    return mapUserToUI(createdUser);
};

export const updateUser = async (id: string, userData: {name: string; email: string}): Promise<UserUI> => {
    const response = await fetch(`${API_URL}/users/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ id, ...userData }),
    });
    
    if(!response.ok) {
        throw new Error('Error al actualizar el usuario en la API');
    }
    
    const updatedUser: ApiUser = await response.json();
    return mapUserToUI(updatedUser);
};

