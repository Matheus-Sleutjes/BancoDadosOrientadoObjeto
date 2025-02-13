import { Routes } from "@angular/router";

export const routes: Routes = [
    {
        path: '',
        title: 'Usuarios',
        loadComponent: async () => (await import('./usuario-listagem/usuario-listagem.component')).UsuarioListagemComponent
    },
    {
        path: 'novo',
        title: 'Novo Usuario',
        loadComponent: async () => (await import('./usuario-novo/usuario-novo.component')).UsuarioNovoComponent
    },
];
