import { test, expect } from '@playwright/test';

const mockUsers = [
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

test.describe('Users E2E Flow', () => {
    test.beforeEach(async ({ page }) => {
        await page.route('**/api/users*', route =>
            route.fulfill({
                status: 200,
                contentType: 'application/json',
                body: JSON.stringify(mockUsers),
            })
        );

        await page.goto('/');
    });

    test('displays edit button on every user row', async ({ page }) => {
        const rows = page.locator('tbody tr');
        await expect(rows.first()).toBeVisible({ timeout: 10000 });

        const rowCount = await rows.count();
        expect(rowCount).toBe(mockUsers.length);

        for (let i = 0; i < rowCount; i++) {
            await expect(rows.nth(i).getByRole('button', { name: /editar/i })).toBeVisible();
        }
    });

    test('opens edit modal populated with user details', async ({ page }) => {
        const firstRow = page.locator('tbody tr').first();
        await expect(firstRow).toBeVisible({ timeout: 10000 });

        await firstRow.getByRole('button', { name: /editar/i }).click();

        await expect(page.getByRole('heading', { name: /editar/i })).toBeVisible();

        const nameInput = page.locator('input[name="name"], input[type="text"]').first();
        const emailInput = page.locator('input[name="email"], input[type="email"]').first();

        await expect(nameInput).toHaveValue(mockUsers[0].name);
        await expect(emailInput).toHaveValue(mockUsers[0].email);
    });
});