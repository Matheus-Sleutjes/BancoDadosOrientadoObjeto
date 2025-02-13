import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
//import { CreateStorie } from '../../../models/create-storie';
import { RealtimeClientService } from '../../../services/realtime-client.service';
import { firstValueFrom, Subscription } from 'rxjs';
import { StoriePlayerDto } from '../../../models/storie-player-dto';
import { Player } from '../../../models/player';
import { Storie } from '../../../models/storie';
//import { Patient } from '../../../models/patient';

@Component({
  standalone: true,  
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrl: './patient-list.component.scss',
  imports: [ReactiveFormsModule, CommonModule, HttpClientModule, FormsModule]
})
export class PatientListComponent implements OnInit, OnDestroy {
  patientOwner : boolean = false;
  storieInPlay: Storie | null = null;
  pokerOptions: string[] | undefined;    
  pokerItemSelected: string | null = null;
  pokerItemSent: boolean = false;        
  players = signal<Player[]>([]);                
  currentUser: StoriePlayerDto = {
    pokerItemSelected: '',
    playerId: '',
    storieId: '',
  };
  cardsSubscription: Subscription | undefined;
  patientTag: string = '';
  storieForm: FormGroup;
  errorMessage: string | null = null;

  constructor(private realtime: RealtimeClientService, private fb: FormBuilder, private route: ActivatedRoute, private http: HttpClient, private router: Router) {
    this.currentUser.playerId = localStorage.getItem('playerId') as unknown as string;
    this.patientOwner = localStorage.getItem('patientOwner') as unknown as boolean;
    this.realtime.connect();
    this.storieForm = this.fb.group({
      description: ['', [Validators.required, Validators.minLength(3)]],
    });
  }
  async ngOnInit(): Promise<void> {
    this.patientTag = this.route.snapshot.paramMap.get('patientTag') ?? '';
    //TODO Extract request in service file
    let patient = await firstValueFrom(this.http.get<any>(`https://localhost:7277/Patient/tag/${this.patientTag}`));
    //TODO Extract request in service file
    let stories = await firstValueFrom(this.http.get<Array<Storie>>(`https://localhost:7277/Patient/${patient.id}/stories?played=false`));
    //TODO Extract request in service file
    let existingPlayers = await firstValueFrom(this.http.get<Array<Player>>(`https://localhost:7277/Player/patient/${patient.id}`));
    this.players.set([...existingPlayers]);
    this.pokerOptions = patient.pokerItems.split(',');
    this.storieInPlay = stories.length > 0 ? stories[0] : null;
    this.cardsSubscription = this.realtime.cardsUpdated$.subscribe(async x => {
      this.players.set([...x]);
    //TODO Extract request in service file
      stories = await firstValueFrom(this.http.get<Array<Storie>>(`https://localhost:7277/Patient/${patient.id}/stories?played=false`));
      this.storieInPlay = stories.length > 0 ? stories[0] : null;
      if(this.allPlayersSubmitted()){
        this.pokerItemSent = false;
        this.pokerItemSelected = null;
      }
    });
    
  }  
  ngOnDestroy(): void {
    this.cardsSubscription?.unsubscribe();
  }
  async onCreateStorie(){
    // if(this.storieForm.valid){
    //   var newStorie : CreateStorie = {
    //     tagPatient : this.patientTag,
    //     description : this.storieForm.get('description')?.value,
    //   };
    //   await this.realtime.createStorie(newStorie);
    // } 
  }
  async submitPokerItem() {
    if (this.pokerItemSelected !== null) {
      this.currentUser.pokerItemSelected = this.pokerItemSelected;
      this.currentUser.storieId = this.storieInPlay?.id ?? "";
      await this.realtime.createStoriePlayer(this.currentUser);  
      this.pokerItemSent = true;
    }
  }
  allPlayersSubmitted(): boolean {
    return this.players().every(player => player.currentStoriePlayed);
  }
}
