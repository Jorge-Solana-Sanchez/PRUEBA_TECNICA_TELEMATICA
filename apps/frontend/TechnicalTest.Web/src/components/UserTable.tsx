import React from 'react';
import type {UserUI} from '../types/user';
import { UserRow } from './UserRow';

interface Props {
    users: UserUI[];
    onDelete: (id: string) => void;
}

export const UserTable: React.FC<Props> = ({ users, onDelete }) => {
    if (users.length === 0) {
        return <p className="no-data">No hay usuarios disponibles.</p>;
    }

    return (
        <table className="user-table">
            <thead>
            <tr>
                <th>Imagen</th>
                <th>Nombre</th>
                <th>Email</th>
                <th>País</th>
                <th>Acciones</th>
            </tr>
            </thead>
            <tbody>
            {users.map((user) => (
                <UserRow key={user.id} user={user} onDelete={onDelete} />
            ))}
            </tbody>
        </table>
    );
};