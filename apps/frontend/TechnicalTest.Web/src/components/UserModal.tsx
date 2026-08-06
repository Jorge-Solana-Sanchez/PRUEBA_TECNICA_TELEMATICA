import React, {useState, useEffect} from 'react';
import type {UserUI, ModalMode} from '../types/user';

interface Props {
    isOpen: boolean;
    mode: ModalMode;
    initialData: UserUI | null;
    onClose: () => void;
    onSave: (userData: Omit<UserUI, 'id' | 'createdAt'>, id?: string) => void;
}

export const UserModal: React.FC<Props> = ({
    isOpen,
    mode,
    initialData,
    onClose,
    onSave,
    
}) =>{
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [country, setCountry] = useState('España');
    
    useEffect(() => {
        if (mode === 'edit' && initialData) {
            setName(initialData.name);
            setEmail(initialData.email);
            setCountry(initialData.country);
        } else {
            setName('');
            setEmail('');
            setCountry('España');
        }
    }, [mode, initialData]);
    
    if (!isOpen) return null;

    const handleSubmit = (e: React.SyntheticEvent) => {
        e.preventDefault();
        if (!name.trim() || !email.trim()) return;

        onSave(
            {
                name,
                email,
                country,
                avatar: initialData?.avatar || `https://api.dicebear.com/7.x/avataaars/svg?seed=${Date.now()}`,
            },
            initialData?.id
        );
        onClose();
    };
    
    return (
        <div className="modal-overlay" onClick={onClose}>
            <div className="modal-content" onClick={(e) => e.stopPropagation()}>
                <h2>{mode === 'edit' ? 'Editar Usuario' : 'Añadir Nuevo Usuario'}</h2>
                
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label>Nombre:</label>
                        <input
                            type="text"
                            required
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            placeholder="Nombre completo"
                        />
                    </div>
                    
                    <div className="form-group">
                        <label>Email:</label>
                        <input
                            type="email"
                            required
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            placeholder="correo@ejemplo.com"
                        />
                    </div>
                    
                    <div className="form-group">
                        <label>País:</label>
                        <input
                            type='text'
                            required
                            value={country}
                            onChange={(e) => setCountry(e.target.value)}
                        />
                    </div>
                    
                    <div className="modal-actions">
                        <button type="button" className="btn-secondary" onClick={onClose}>
                            Cancelar
                        </button>
                        <button type="submit" className="btn-primary">
                            {mode === 'edit' ? 'Guardar Cambios' : 'Crear Usuario'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};