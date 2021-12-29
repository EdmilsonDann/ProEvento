import { FormGroup, FormBuilder, Validators, AbstractControlOptions } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ValidatorField } from '@app/helpers/ValidatorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  form!: FormGroup;

  get f(): any{
    return this.form.controls;
  }

  constructor(public fb: FormBuilder) { }

  ngOnInit() {
    this.validation();
  }

  public validation():void{

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('senha','confirmeSenha')
    };

    this.form = this.fb.group({
      titulo: ['',[Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      primeiroNome: ['',[Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      ultimoNome: ['',Validators.required],
      email: ['',[Validators.required, Validators.email]],
      telefone: ['',Validators.required],
      funcao: ['',[Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      descricao: ['',[Validators.required, Validators.minLength(4), Validators.maxLength(100)]],
      senha: ['',[Validators.required, Validators.minLength(6)]],
      confirmeSenha: ['',[Validators.required]]
    }, formOptions);
  }

  onSubmit(): void{
    if(this.form.invalid){
      return;
    }
  }

  public resetForm(event: any) : void{
    event.preventDefault(); //isso signifca..nao faça o padrao que é atualizar a página, "sem isso ele vai ficar dando refresh na página"
    this.form.reset();
  }

}
