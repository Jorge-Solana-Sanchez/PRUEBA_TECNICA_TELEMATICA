import React from 'react';
import type {UserUI} from '../types/user';

interface Props {
    user: UserUI;
    onEdit: (user: UserUI) => void;
    onDelete: (id: string) => void;
}

export const UserRow: React.FC<Props> = ({user, onEdit, onDelete}) => {
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
                <div className="action-buttons">
                    <button className="btn-edit" onClick={() => onEdit(user)}>
                        Editar
                    </button>
                    <button className="btn-delete" onClick={() => onDelete(user.id)}>
                        Eliminar
                    </button>
                </div>
                
            </td>
        </tr>
    );
};