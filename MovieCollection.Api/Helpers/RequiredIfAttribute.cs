using System;
using System.ComponentModel.DataAnnotations;

namespace MovieCollection.Api.Helpers;
// googlet meg frem til dette uten full kontroll av hva den gjør, husk å studere ValidationAttribute

//sjekker om Id har en verdi (eksisterende entity), og om den er null sjekker at denne nye entitien inkluderer et navn
public class RequiredIfIdNullAttribute(string otherProperty) : ValidationAttribute
{
    private readonly string _otherProperty = otherProperty;

    // sjekker om Id eksisterer i det hele tatt, må da være overflødig når Id er en primary key og alltid eksisterer?
    // men something something best practice, tipper jeg
    // PS. sjekk hva ValidationContext faktisk er/gjør
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) 
    {
        var otherProperty = validationContext.ObjectType.GetProperty(_otherProperty);
        if(otherProperty == null){
            return new ValidationResult($"Unknown property: {_otherProperty}");
        }

        var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance); //sjekker hele Dto-en på en eller annen måte??
        
        if(otherPropertyValue == null) // sjekker om id er null
        {
            if(value== null) //sjekker at Name ikke også er null
            {
                return new ValidationResult($"{validationContext.DisplayName} is required");
            }
        }
        return ValidationResult.Success; //sier i fra at alt er i orden
    }
}
