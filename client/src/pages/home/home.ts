import { inject } from 'aurelia-framework';
import { ApplicantService } from '../../services/applicant-service';
import { ValidationController, ValidationControllerFactory, ValidationRules, Validator } from 'aurelia-validation';
import { DialogService } from 'aurelia-dialog';
import { Confirmation } from '../../dialogs/confirmation';
import { Result } from '../../dialogs/result';
import { I18N } from 'aurelia-i18n';
import { Router } from 'aurelia-router';

@inject(ValidationControllerFactory, Validator, ApplicantService, DialogService, I18N, Router)
export class Home {
    formData = new FormData();
    formErrors: any;
    isSubmitting = false;
    canSubmit = false;
    canReset = false;

    validationController: ValidationController;
    applicantService: ApplicantService;
    validator: Validator;

    dialogService: DialogService;
    i18n: I18N;
    router: Router;

    constructor(
        validationControllerFactory: ValidationControllerFactory, 
        validator: Validator, 
        applicantService: ApplicantService,
        dialogService: DialogService,
        i18N: I18N,
        router: Router) {
        this.validator = validator;
        this.validationController = validationControllerFactory.createForCurrentScope(validator);
        this.applicantService = applicantService;
        this.dialogService = dialogService;
        this.i18n = i18N;
        this.router = router;
        this.validationController.subscribe(e => { this.validateForm(); this.isFormEmpty() });
        ValidationRules
            .ensure('name')
                .minLength(5)
                .maxLength(50)
                .required()
            .ensure('familyName')
                .minLength(5)
                .maxLength(50)
                .required()
            .ensure('address')
                .minLength(10)
                .maxLength(50)
                .required()
            .ensure('countryOfOrigin')
                .maxLength(50)
                .required()
            .ensure('eMailAddress')
                .maxLength(50)
                .required()
            .ensure('age')
                .matches(/^\d+$/).withMessage(`Age must be a whole number`)
                .required()
                .then()
                .satisfies((v, _) => parseInt(v) >= 20 && parseInt(v) <= 60).withMessage('Age must be between 20 and 60')
            .on(this.formData);
    }

    validateForm() {
        this.validator.validateObject(this.formData)
            .then(results => this.canSubmit = results.every(result => result.valid));
    }

    isFormEmpty() {
       this.canReset = !this.formData.isEmpty()
    }

    onSubmit() {
        this.submitApplicant();
    }

    submitApplicant() {
        this.isSubmitting = true;
        this.applicantService.saveApplicant({
            name: this.formData.name,
            familyName: this.formData.familyName,
            address: this.formData.address,
            countryOfOrigin: this.formData.countryOfOrigin,
            eMailAddress: this.formData.eMailAddress,
            age: parseInt(this.formData.age),
            isHired: this.formData.isHired ? true : false
        }).then(response => response.json())
            .then(this.onSuccessfulSubmission.bind(this))
            .catch(this.onFailedSubmission.bind(this))
            .finally(() => { this.isSubmitting = false })
    }

    onSuccessfulSubmission(response) {
        this.router.navigate('success');
    }

    onFailedSubmission(error) {
        this.dialogService.open({ viewModel: Result, model: error })
    }

    onReset() {
        this.dialogService.open({ viewModel: Confirmation, })
            .whenClosed(response => {
                if (!response.wasCancelled) {
                    this.formData.clear();
                }
            });
    }
}

class FormData {

    constructor(
        public name = '', 
        public familyName = '',
        public address = '', 
        public countryOfOrigin = '', 
        public eMailAddress = '', 
        public age = '', 
        public isHired = false) {}

    clear() {
        this.name = '';
        this.familyName = '';
        this.address = '';
        this.countryOfOrigin = '';
        this.eMailAddress = '';
        this.age = '';
        this.isHired = false;
    }

    isEmpty() {
        return this.name.length == 0 && this.familyName.length == 0 &&
            this.address.length == 0 && this.countryOfOrigin.length == 0 &&
            this.eMailAddress.length == 0 && this.age.length == 0
    }
}
