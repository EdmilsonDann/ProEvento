import { AbstractControl, FormGroup } from "@angular/forms";

export class ValidatorField {
  //por causa do return null coloquei o "any" como retorno do método.
  static MustMatch(controlName: string, matchingControlName: string): any{
    return (group: AbstractControl) => {
      const formGroup = group as FormGroup;
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if(matchingControl.errors && !matchingControl.errors.mustMatch){
        return null;
      }

      if(control.value !== matchingControl.value){
        matchingControl.setErrors({mustMatch: true}); //aqui estou criando um campo novo dentro do matchingControl
      }
      else{
        matchingControl.setErrors(null);
      }

      return null;
    }
  }
}
