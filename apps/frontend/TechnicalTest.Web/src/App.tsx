import { useEffect, useState } from 'react';
import type { ModalMode, UserUI } from './types/user';
import { getUsers, createUser, updateUser } from './services/userService';
import { UserTable } from './components/UserTable';
import { UserModal } from './components/UserModal';
import './App.css';

export function App() {
    const [originalUsers, setOriginalUsers] = useState<UserUI[]>([]);
    const [users, setUsers] = useState<UserUI[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    const [modalMode, setModalMode] = useState<ModalMode>(null);
    const [selectedUser, setSelectedUser] = useState<UserUI | null>(null);

    useEffect(() => {
        getUsers()
            .then((data) => {
                setOriginalUsers(data);
                setUsers(data);
                setLoading(false);
            })
            .catch((err) => {
                setError(err.message);
                setLoading(false);
            });
    }, []);

    const handleRestore = () => {
        setUsers([...originalUsers]);
    };

    const handleOpenAddModal = () => {
        setSelectedUser(null);
        setModalMode('add');
    };

    const handleOpenEditModal = (user: UserUI) => {
        setSelectedUser(user);
        setModalMode('edit');
    };

    const handleCloseModal = () => {
        setModalMode(null);
        setSelectedUser(null);
    };

    const handleSaveUser = async (userData: Omit<UserUI, 'id' | 'createdAt'>, id?: string) => {
        try {
            if (modalMode === 'edit' && id) {
                const updatedUser = await updateUser(id, {
                    name: userData.name,
                    email: userData.email
                });
                setUsers((prev) =>
                    prev.map((u) => (u.id === id ? updatedUser : u))
                );
            } else if (modalMode === 'add') {
                const createdUser = await createUser({
                    name: userData.name,
                    email: userData.email
                });
                setUsers((prev) => [createdUser, ...prev]);
            }
            handleCloseModal();
        } catch (err: any) {
            alert(`Error al guardar en la API: ${err.message}`);
        }
    };

    const handleDelete = (id: string) => {
        setUsers((prevUsers) => prevUsers.filter((u) => u.id !== id));
    };

    return (
        <div className="container">
            <div className="header-actions">
                <h1>Gestión de Usuarios</h1>
                <div className="global-buttons">
                    <button className="btn-add" onClick={handleOpenAddModal}>
                        + Añadir Usuario
                    </button>
                    <button className="btn-restore" onClick={handleRestore}>
                        Restaurar
                    </button>
                </div>
            </div>

            {loading && <p>Cargando usuarios...</p>}
            {error && <p className="error">Error: {error}</p>}

            {!loading && !error && (
                <UserTable
                    users={users}
                    onEdit={handleOpenEditModal}
                    onDelete={handleDelete}
                />
            )}

            <UserModal
                isOpen={modalMode !== null}
                mode={modalMode}
                initialData={selectedUser}
                onClose={handleCloseModal}
                onSave={handleSaveUser}
            />
        </div>
    );
}