namespace Practice.Indications;

public record Cornea(CorneaValue SeMean, CorneaValue K1, CorneaValue K2, CorneaValue DeltaKMean,
    List<CorneaValue> Sd, List<CorneaValue> Se, List<CorneaValue> DeltaK);