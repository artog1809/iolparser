namespace Practice.Indications;

public record Cornea(CorneaValue SeMean, CorneaValue K1, CorneaValue K2, CorneaValue deltaKMean,
    List<CorneaValue> Sd, List<CorneaValue> Se, List<CorneaValue> deltaK);