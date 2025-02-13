import { Routes } from "@angular/router";

export const routes: Routes = [
    {
        path: 'create',
        title: 'Patient Create',
        loadComponent: async () => (await import('./patient-create/patient-create.component')).PatientCreateComponent
    },
    {
        path: '',
        title: 'Patient List',
        loadComponent: async () => (await import('./patient-list/patient-list.component')).PatientListComponent
    },
    {
        path: ':id',
        title: 'Patient Details',
        loadComponent: async () => (await import('./patient-details/patient-details.component')).PatientDetailsComponent
    },
];