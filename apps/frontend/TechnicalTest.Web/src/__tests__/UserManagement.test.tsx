import { render, screen } from '@testing-library/react';
import { UserTable } from '../components/UserTable';
import { UserModal } from '../components/UserModal';
import type { UserUI } from '../types/user';

const mockUsers: UserUI[] = [
    {
        id: '1',
        name: 'Juan Pérez',
        email: 'juan@example.com',
        country: 'España',
        avatar: 'https://example.com/avatar1.png',
        createdAt: '2026-01-01T00:00:00.000Z',
    },
    {
        id: '2',
        name: 'Ana López',
        email: 'ana@example.com',
        country: 'España',
        avatar: 'https://example.com/avatar2.png',
        createdAt: '2026-01-02T00:00:00.000Z',
    },
];

describe('UserManagement Component Tests', () => {
    const noop = () => {};

    test('should render edit button for each user row', () => {
        render(
            <UserTable
                users={mockUsers}
                onEdit={noop}
                onDelete={noop}
            />
        );

        const editButtons = screen.getAllByRole('button', { name: /editar/i });
        expect(editButtons).toHaveLength(mockUsers.length);
    });

    test('should open modal with user data on edit action', () => {
        const user = mockUsers[0];

        render(
            <UserModal
                isOpen={true}
                mode="edit"
                initialData={user}
                onClose={noop}
                onSave={noop}
            />
        );

        expect(screen.getByRole('heading', { name: /editar/i })).toBeInTheDocument();
        expect(screen.getByDisplayValue(user.name)).toBeInTheDocument();
        expect(screen.getByDisplayValue(user.email)).toBeInTheDocument();
    });
});