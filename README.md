   Descrevendo em 1 paragrafo o que precisei fazer para adaptar a nova categoria:
   
   Criei a propriedade bool IsPoliticallyExposed na interface ITrade,na assinatura do Trade, na validação no TradeCategory.
  
   Assumi que para a nova categoria, o texto também viria com true/false após a NextPaymentDate
   assim verifico se é PEP antes das outras validações, aceitando 2 formatos de textos colado no console
   
   COM PEP
   12/11/2020
    4
    2000000 Private 12/29/2025 true 
    400000 Public 07/01/2020 false
    5000000 Public 01/02/2024 false
    3000000 Public 10/26/2023 false

    SEM PEP 
    12/11/2020
    4
    2000000 Private 12/29/2025
    400000 Public 07/01/2020
    5000000 Public 01/02/2024
    3000000 Public 10/26/2023
  
