import React from 'react';
import type {UserUI} from '../types/user';

interface Props {
    user: UserUI;
    onDelete: (id: string) => void;
}

export const UserRow: React.FC<Props> = ({user, onDelete}) => {
    return (
        <tr>
            <td>
                <img
                    src={user.avatar}
                    alt={user.name}
                    style={{width: '40px', height: '40px', borderRadius: '50%'}}
                />
            </td>
            <td>{user.name}</td>
            <td>{user.email}</td>
            <td>{user.country}</td>
            <td>
                <button
                    className="btn-delete"
                    onClick={() => onDelete(user.id)}
                    >
                    Eliminar
                </button>
            </td>
        </tr>
    );
};