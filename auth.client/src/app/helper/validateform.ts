import { FormControl, FormGroup } from "@angular/forms";

export default class ValidateForm {
     static validateAllForms(formsFroup:FormGroup){
        Object.keys(formsFroup.controls).forEach(field=>{
          const controls = formsFroup.get(field);
          if(controls instanceof FormControl){
            controls.markAsDirty({onlySelf:true});
          }
          else if(controls instanceof FormGroup){
            this.validateAllForms(controls);
          }
        })
      }
}