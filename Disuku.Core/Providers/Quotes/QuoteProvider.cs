﻿using System.Collections.Generic;
using Disuku.Core.Entities;
using Disuku.Core.Storage;

namespace Disuku.Core.Providers.Quotes
{
    public class QuoteProvider : IQuoteProvider
    {
        private readonly IDataStore _dataStore;
        private const string TableName = "Quotes";
        public QuoteProvider(IDataStore dataStore)
        {
            _dataStore = dataStore;
            _dataStore.InitializeDbAsync("DisukuBot");
        }
