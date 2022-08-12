Select t.*, Rowid From niff_ads_valoresmetas t
Where idempresa Not In (9,11)
And
 referencia > 201712
And idmetas In ( 57,72,102 )
And previsto = 0 And realizado <> 0
Order By referencia, idmetas