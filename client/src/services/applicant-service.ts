import { HttpClient } from 'aurelia-fetch-client';
import { Constants } from '../constants';

export class ApplicantService {
    private httpClient = new HttpClient();

    saveApplicant(requestPayload: SaveApplicantRequest) {
        return this.httpClient.post(`${Constants.apiBaseUrl}/Applicant`, JSON.stringify({
            name: requestPayload.name,
            familyName: requestPayload.familyName,
            address: requestPayload.address,
            countryOfOrigin: requestPayload.countryOfOrigin,
            eMailAddress: requestPayload.eMailAddress,
            age: requestPayload.age,
            isHired: requestPayload.isHired
        }), {
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        });
    }
}

export interface SaveApplicantRequest {
    name: string;
    familyName: string;
    address: string;
    countryOfOrigin: string;
    eMailAddress: string;
    age: number;
    isHired: boolean;
}