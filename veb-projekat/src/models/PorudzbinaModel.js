export class PorudzbinaModel {
    constructor(ukupnaCijena ,adresaDostave, kolicina, datumPorudzbine, vrijemeDostave, cijenaDostave) {
        this.ukupnaCijena = ukupnaCijena;
        this.adresaDostave = adresaDostave;
        this.kolicina = kolicina;
        this.datumPorudzbine = datumPorudzbine;
        this.vrijemeDostave = vrijemeDostave;
        this.cijenaDostave = cijenaDostave;
    }
}
export default PorudzbinaModel;